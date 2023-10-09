
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/temperatureHub")
    .build();

connection.on("ReceiveTemperature", function (temperature) {
    let tempElement = document.createElement("div");
    tempElement.innerHTML = `Temperature: ${temperature}°C`;
    document.getElementById("temperatureData").appendChild(tempElement);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
