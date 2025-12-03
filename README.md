# MES Heartbeat Demo

這是一個用於 **MES ↔ 設備通信** 的 Heartbeat（心跳包）專案。  
透過 Server / Client 架構，展示設備如何定期上傳狀態、保持連線存活，並使用 TCP/IP socket 通訊流程。

---

## 🔧 專案功能

- ✔ Heartbeat Client 每 2 秒傳送心跳訊號  
- ✔ Heartbeat Server 可接收多次連線並顯示訊息  
- ✔ 支援 Start / Stop，釋放連線資源  
- ✔ 非同步接受 Client（BeginAcceptTcpClient）  
- ✔ Heartbeat thread 安全結束，不會殘留背景程序  

---

## 📂 專案結構

```
mes-heartbeat-demo/
 ├── src/
 │    ├── Program.cs            # 入口程式，可選 Server / Client
 │    ├── HeartbeatServer.cs    # Heartbeat Server 邏輯
 │    ├── HeartbeatClient.cs    # Heartbeat Client 邏輯
 ├── README.md
 ├── LICENSE
 └── .gitignore
```

---

## 🚀 使用方式

### 啟動方式（兩個終端視窗）

#### 🖥 視窗 A：啟動 Server
```
dotnet run
```
輸入：
```
1
```

#### 🖥 視窗 B：啟動 Client
若 exe 被佔用可使用：
```
dotnet run
```
輸入：
```
2
```

---

## 📡 Heartbeat 行為示例

### **Client 端程式碼示例**

```csharp
var client = new HeartbeatClient();
client.Start("127.0.0.1", 9999);

// 程式中會每 2 秒自動送一次 HEARTBEAT
Console.ReadLine();
client.Stop();
```

### **Server 收到訊息示例**

```
[SERVER] Heartbeat server started on port 9999
[SERVER] Client connected.
[SERVER] Received: HEARTBEAT
[SERVER] Received: HEARTBEAT
```

---

## 🧠 技術亮點

- 基於 TCP/IP Socket 通訊模型  
- 使用非同步 `BeginAcceptTcpClient` 提升可擴展性  
- 心跳執行緒安全停止，避免 thread 殘留
- 資源管理完整：NetworkStream / TcpClient / TcpListener 都可正確關閉  
- 適合設備端 Heartbeat 機制與連線維持流程  

---

## 🏭 適用場景

- MES ↔ 設備心跳通訊測試  
- 設備存活偵測  
- 工廠自動化／IPC 通訊教學
- 搭配主系統監控設備連線狀態  

---

## 👤 作者

HungHsiang, Lin（林弘翔）  
Software Engineer — MES / Equipment Communication / Automation  