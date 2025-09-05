import * as signalR from "@microsoft/signalr";
import { getAccessToken } from "./tokenService";

let connection: signalR.HubConnection;

export const startNotificationConnection = async (
  teacherId: number,
  onNotificationReceived: (notification: any) => void
) => {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7025/notificationHub", {
      accessTokenFactory: () => getAccessToken() || "",
    })
    .withAutomaticReconnect()
    .build();

  // ✅ inline-обработчик с логом
  connection.on("ReceiveEnrollmentNotification", (notification) => {
    console.log("Received notification:", notification); // логируем тут
    onNotificationReceived(notification); // передаём дальше в React
  });

  try {
    await connection.start();
    console.log("SignalR connected");

    await connection.invoke("JoinGroup", `Teacher_${teacherId}`);
    console.log(`Joined group: Teacher_${teacherId}`);
  } catch (err) {
    console.error("Error connecting to SignalR", err);
  }
};

export const stopNotificationConnection = async () => {
  if (connection) {
    await connection.stop();
    console.log("SignalR disconnected");
  }
};
