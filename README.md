# 🏎️ Gokart Timer – Father & Son Hackathon Project

**Gokart Timer** is a demo project created during an 8-hour father & son hackathon.  
The goal: build a simple system to **simulate go-kart races** with a backend (Rust)  
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

## 🖥️ Frontend Setup & Running

### Setup
1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Restore NuGet packages:
   ```bash
   make restore
   ```

### Running the Application
- **Debug mode**:
   ```bash
   make run
   ```

- **Release mode**:
   ```bash
   make run-release
   ```

### Building
- **Debug build**:
   ```bash
   make build
   ```

- **Release build**:
   ```bash
   make release
   ```

### Testing
```bash
make test
```

### Creating Deployable Packages
To create self-contained executables for all platforms:
```bash
make publish
```

This will generate executables in the `artifacts` directory for Windows, Linux, and macOS.

---

## 🖥️ Backend Setup & Running

### Setup
1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Install Rust dependencies:
   ```bash
   cargo build
   ```

### Running the Application
- **Development mode**:
   ```bash
   cargo run
   ```

- **Release mode**:
   ```bash
   cargo run --release
   ```

### Building
- **Debug build**:
   ```bash
   cargo build
   ```

- **Release build**:
   ```bash
   cargo build --release
   ```

### Testing
```bash
cargo test
```

### API Endpoints
The backend server will start on `http://localhost:3000` with the following endpoints:
- `GET /ping` - Health check
- `GET /times` - Get all lap times
- `POST /start-session` - Start a new race session
- `POST /lap` - Record a new lap time
- `GET /summary` - Get race summary statistics

### Configuration
The server configuration can be modified in `src/main.rs` or through environment variables:
- `PORT` - Server port (default: 3000)
- `HOST` - Server host (default: 0.0.0.0)

---

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

