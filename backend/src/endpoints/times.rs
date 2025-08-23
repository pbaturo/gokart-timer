// /times
// [
//   {
//     "id": 1,
//     "time": 53434321 (milis)
//     "lap": 1
//   },
//   {
//     "id": 2,
//     "time": 1232322 (milis)
//     "lap": 1
//   }
// ]

use axum::{Json, extract::State};

use crate::{AppState, Time};

pub async fn times(State(state): State<AppState>) -> Json<Vec<Time>> {
    Json(state.times)
}
