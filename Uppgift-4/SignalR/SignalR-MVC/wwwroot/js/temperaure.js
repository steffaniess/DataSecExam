let connection = new signalR.HubConnectionBuilder()
    .withUrl("/temperatureHub")
    .build();

    //eventhandlers
connection.on("ReceiveTemperature", function (temperature) {
    console.log("Received temperature from server", temperature);

    // Update the temperatureData div with the new temperature
    let temperatureDataDiv = document.getElementById("temperatureData");

    // When the first data comes, clear the "waiting for temperature" message
    temperatureDataDiv.innerHTML = "";

    let tempElement = document.createElement("div");
    tempElement.innerHTML = `Temperature: ${temperature}°C`;
    temperatureDataDiv.appendChild(tempElement);
});

connection.onClose(() => {
    console.error('SignalR connection closed');
});

connection.onreconnecting(() => {
    console.log('Reconnection to SignalR hub');
});

//Start connection
connection.start()
    .then(() => console.log('Connected to the SignalR Hub'))
    .catch(function (err) {
        return console.error(err.toString());
    });
