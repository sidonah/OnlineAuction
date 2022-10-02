"use strict"

const connection = new signalR.HubConnectionBuilder().withUrl("/BestBid").build();

connection.on("RecieveBestBid", function (message) {
    var bestPrice = document.getElementById("showBestPrice")
    if (parseInt(bestPrice.textContent) < parseInt(message)) {
        bestPrice.textContent = message + " birr"
        document.getElementById("DisplayError").textContent = ""
    }
    else
        document.getElementById("DisplayError").textContent = "The price you put in did not beat the best price"
        
})

connection.start().catch((err) => console.error(err.toString()))
document.getElementById("send").addEventListener("click", (event) => {
    var message = document.getElementById("bid").value;
    var lotId = document.getElementById("lotId").textContent;
    console.log(lotId);
    connection.invoke("SendMessageToAll", message, lotId).catch((err) => {
        return console.error(err.toString());
    })  
    event.preventDefault();
})