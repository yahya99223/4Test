(function (controllers) {
   
    var captureStudio_journeyController = require('../captureStudio/api/controllers/journeyController.js');

    controllers.init = function (app,helpers) {
        captureStudio_journeyController.init(app,helpers);
    };

})(module.exports);
