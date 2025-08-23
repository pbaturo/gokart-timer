use serde::Deserialize;
use serde::Serialize;

use axum::{Router, routing::get};

mod endpoints;

#[derive(Clone, Copy, Serialize, Deserialize)]
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
    let app = app();

    let listener = tokio::net::TcpListener::bind("0.0.0.0:3000").await.unwrap();

    axum::serve(listener, app).await.unwrap();
}

fn app() -> Router {
    let state = AppState::new();
    Router::new()
        .route("/ping", get(endpoints::ping))
        .route("/times", get(endpoints::times))
        .with_state(state)
}
