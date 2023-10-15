let connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7196/temperatureHub")
    .build();

// Event-handlers
connection.on("ReceiveTemperature", function (deviceId, encryptedTemperature) {
    console.log(`Received encrypted temperature from device ${deviceId}: ${encryptedTemperature}`);

    // Identifying the correct device based on deviceId
    let deviceDiv = document.getElementById(deviceId);
    if (!deviceDiv) {
        console.warn(`Device div with ID ${deviceId} not found.`);
        return;
    }

    let temperatureDataDiv = deviceDiv.querySelector(".temperatureData");

    // clear any previous messages
    temperatureDataDiv.innerHTML = `Encrypted Temperature: ${encryptedTemperature}`;
});

//connection.onClose(() => {
//    console.error('SignalR connection closed');
//});

connection.onreconnected(() => {
    console.log('Reconnecting to SignalR hub');
});

// Start connection
connection.start()
    .then(() => console.log('Connected to the SignalR Hub'))
    .catch(function (err) {
        return console.error(err.toString());
    });
