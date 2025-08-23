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

