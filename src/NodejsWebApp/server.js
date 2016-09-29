var http = require('http');
var port = process.env.port || 1337;
console.log(port);

var express = require('express');
var app = express();

app.get("/", function(req, res) {
    res.send("Hello world");
});

var server = http.createServer(app);
server.listen(port);