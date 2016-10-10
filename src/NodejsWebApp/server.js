var http = require('http');
var port = process.env.port || 1337;
var express = require('express');
var app = express();

var helpers = require('./src/infrastructure')();

var controllers = require('./src/controllers');
controllers.init(app, helpers);


var server = http.createServer(app);
server.listen(port);