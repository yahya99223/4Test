module.exports = function () {
    var helpers=new Object();

    var guid = require('./guid');
    guid.init(helpers);

    return helpers;
};