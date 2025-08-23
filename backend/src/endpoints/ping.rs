pub async fn ping() -> String {
    chrono::offset::Local::now()
        .format("%d-%m-%Y %H:%M:%S %Z")
        .to_string()
}

#[tokio::test]
async fn ping_test() {
    assert_eq!(
        ping().await,
        chrono::offset::Local::now()
            .format("%d-%m-%Y %H:%M:%S %Z")
            .to_string()
    );
}
