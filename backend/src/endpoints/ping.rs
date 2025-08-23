pub async fn ping() -> String {
    let time = chrono::offset::Local::now()
        .format("%d-%m-%Y %H:%M:%S %Z")
        .to_string();

    tracing::debug!("sending time data: {}", time);
    time
}
