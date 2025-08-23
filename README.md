# 🏎️ Gokart Timer – Father & Son Hackathon Project

**Gokart Timer** is a demo project created during an 8-hour father & son hackathon.  
The goal: build a simple system to **simulate go-kart races** with a backend (Rust *or* Haskell)  
and a frontend (.NET), connected via a lightweight REST API.

---

## 🚀 Features (MVP)
- Start a new race session (`/start-session`)
- Simulate laps with randomised times and speeds (`/lap`)
- Retrieve race summary: best/average lap, lap breakdown (`/summary`)
- Frontend UI to start races, view laps, and show a basic chart/summary
- Configurable backend implementation (Rust or Haskell)

---

## ⚙️ Prerequisites
- [Git](https://git-scm.com/)  
- [Rust](https://www.rust-lang.org/) *or* [Haskell Stack](https://docs.haskellstack.org/)  
- [.NET 8 SDK](https://dotnet.microsoft.com/download)  

---

## 📑 API Sketch
- `GET /health` → `{ "status": "ok" }`
- `POST /start-session` → `{ "sessionId": "uuid" }`
- `POST /lap { sessionId }` →  
  `{ "lapNo": 1, "timeMs": 58231, "avgSpeedKmh": 46.2 }`
- `GET /summary?sessionId=uuid` →  
  `{ "bestMs": ..., "avgMs": ..., "laps": [...] }`

See [`docs/API.md`](docs/API.md) and [`contracts/lap.schema.json`](contracts/lap.schema.json) for details.

---

## 👨‍👦 Hackathon Goals
- Learn how frontend ↔ backend communication works  
- Practice Rust/Haskell + .NET in one project  
- Collaborate: son = backend, father = frontend  
- Have fun with simulated racing!

---

## 📜 License
This project is released under the [MIT License](LICENSE).

