use axum::{Router, routing::get};

mod endpoints;

#[tokio::main]
async fn main() {
    let app = Router::new().route("/ping", get(endpoints::ping));

    let listener = tokio::net::TcpListener::bind("0.0.0.0:3000").await.unwrap();

    axum::serve(listener, app).await.unwrap();
}
