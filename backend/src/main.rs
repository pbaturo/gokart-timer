// TODO: Write tests using tower

use axum::{
    Router,
    extract::{MatchedPath, Request},
    routing::get,
};
use serde::{Deserialize, Serialize};
use tower_http::trace::TraceLayer;
use tracing_subscriber::{layer::SubscriberExt, util::SubscriberInitExt};

mod endpoints;

#[derive(Clone, Copy, Serialize, Deserialize, Debug)]
struct Time {
    id: u8,
    time: u32,
    lap: u8,
}

#[derive(Clone)]
struct AppState {
    times: Vec<Time>,
}

impl AppState {
    // TODO: Remove dummy data and replace it with a time adding system
    fn new() -> Self {
        AppState {
            times: vec![
                Time {
                    id: 1,
                    time: 333333,
                    lap: 1,
                },
                Time {
                    id: 2,
                    time: 323232,
                    lap: 1,
                },
            ],
        }
    }
}

#[tokio::main]
async fn main() {
    tracing_subscriber::registry()
        .with(
            tracing_subscriber::EnvFilter::try_from_default_env().unwrap_or_else(|_| {
                format!("{}=info,tower_http=info", env!("CARGO_CRATE_NAME")).into()
            }),
        )
        .with(tracing_subscriber::fmt::layer())
        .init();
    let app = app();

    let listener = tokio::net::TcpListener::bind("0.0.0.0:3000").await.unwrap();
    tracing::info!("listening on {}", listener.local_addr().unwrap());

    axum::serve(listener, app).await.unwrap();
}

fn app() -> Router {
    let state = AppState::new();
    Router::new()
        .route("/ping", get(endpoints::ping))
        .route("/times", get(endpoints::times))
        .layer(TraceLayer::new_for_http().make_span_with(|req: &Request| {
            let method = req.method();
            let uri = req.uri();

            let matched_path = req
                .extensions()
                .get::<MatchedPath>()
                .map(|matched_path| matched_path.as_str());

            tracing::debug_span!("request", %method, %uri, matched_path)
        }))
        .with_state(state)
}
