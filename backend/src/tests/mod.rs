use axum::{
    body::Body,
    http::{Request, StatusCode},
};
use http_body_util::BodyExt;
use tower::ServiceExt;

use super::*;

#[tokio::test]
async fn ping_test() {
    let app = app();
    let response = app
        .oneshot(Request::builder().uri("/ping").body(Body::empty()).unwrap())
        .await
        .unwrap();
    let time = chrono::offset::Local::now()
        .format("%d-%m-%Y %H:%M:%S %Z")
        .to_string();

    assert_eq!(response.status(), StatusCode::OK);

    let body = response.into_body().collect().await.unwrap().to_bytes();

    assert_eq!(&body[..], time.into_bytes());
}
