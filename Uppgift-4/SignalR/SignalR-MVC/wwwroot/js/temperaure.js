// temperature.js
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/temperatureHub")
    .build();

connection.on("ReceiveTemperature", function (temperature) {
    const tempElement = document.getElementById("temperatureData");
    tempElement.textContent = temperature + "°C";
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
