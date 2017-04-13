/// <reference path="../Scripts/jquery-3.1.1.intellisense.js" />
/// <reference path="../Scripts/jquery.signalR-2.2.1.js" />

// Crockford's supplant method (poor man's templating)
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

$(function () {

    var chatHub = $.connection.chat, // the generated client-side hub proxy

        $txtMessage = $('#txtMessage');
    $txtAllMessages = $('#txtAllMessages');
    $btnSend = $('#btnSend');
        

    // Add client-side hub methods that the server will call
    $.extend(chatHub.client, {
        updateStockPrice: function (stock) {
            var displayStock = formatStock(stock),
                $row = $(rowTemplate.supplant(displayStock)),
                $li = $(liTemplate.supplant(displayStock)),
                bg = stock.LastChange < 0
                        ? '255,148,148' // red
                        : '154,240,117'; // green

            $stockTableBody.find('tr[data-symbol=' + stock.Symbol + ']')
                .replaceWith($row);
            $stockTickerUl.find('li[data-symbol=' + stock.Symbol + ']')
                .replaceWith($li);

            $row.flash(bg, 1000);
            $li.flash(bg, 1000);
        },

        marketOpened: function () {
            $("#open").prop("disabled", true);
            $("#close").prop("disabled", false);
            $("#reset").prop("disabled", true);
            scrollTicker();
        },

        marketClosed: function () {
            $("#open").prop("disabled", false);
            $("#close").prop("disabled", true);
            $("#reset").prop("disabled", false);
            stopTicker();
        },

        marketReset: function () {
            return init();
        }
    });

    // Start the connection
    $.connection.hub.start()
        .then(init)
        .then(function () {
            return chatHub.server.getMarketState();
        })
        .done(function (state) {
            if (state === 'Open') {
                chatHub.client.marketOpened();
            } else {
                chatHub.client.marketClosed();
            }

            // Wire up the buttons
            $("#open").click(function () {
                chatHub.server.openMarket();
            });

            $("#close").click(function () {
                chatHub.server.closeMarket();
            });

            $("#reset").click(function () {
                chatHub.server.reset();
            });
        });
});