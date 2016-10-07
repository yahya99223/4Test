(function (journeyController) {

    journeyController.init = function (app, helpers) {

        app.post("/captureStudio/journey/start", function (req, res) {            
            var result = helpers.createNewGuid();
            console.log(result);
            res.send(result);
        });

    };
})(module.exports); 

