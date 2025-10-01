function getStatistics(endpoint) {

    $.get(endpoint, (data, status) => {
        if (status == "success") {

            var ctx6 = $("#doughnut-chart").get(0).getContext("2d");
            var myChart6 = new Chart(ctx6, {
                type: "doughnut",
                data: {
                    labels: ["Land", "Farmland", "Commercial Unit", "Shop", "Apartment"],
                    datasets: [{
                        backgroundColor: [
                            "rgba(76, 185, 231, .7)",
                            "rgba(76, 185, 231, .6)",
                            "rgba(76, 185, 231, .5)",
                            "rgba(76, 185, 231, .4)",
                            "rgba(76, 185, 231, .3)"
                        ],
                        data: [
                            data.countOfLandListings,
                            data.countOfFarmlandListings,
                            data.countOfCommercialUnitListings,
                            data.countOfShopListings,
                            data.countOfApartmentListings
                        ]
                    }]
                },
                options: {
                    responsive: true
                }
            });

            var ctx7 = $("#doughnut-chart2").get(0).getContext("2d");
            var myChart7 = new Chart(ctx7, {
                type: "doughnut",
                data: {
                    labels: data.countOfListingsPropertyStatuses.map(x => x.propertyStatus),
                    datasets: [{
                        backgroundColor: data.countOfListingsPropertyStatuses.map(x => x.rgbColor),
                        data: data.countOfListingsPropertyStatuses.map(x => x.count)
                    }]
                },
                options: {
                    responsive: true
                }
            });

            var ctx8 = $("#doughnut-chart3").get(0).getContext("2d");
            var myChart8 = new Chart(ctx8, {
                type: "doughnut",
                data: {
                    labels: ["Land", "Farmland", "Commercial Unit", "Shop", "Apartment"],
                    datasets: [{
                        backgroundColor: [
                            "rgba(76, 185, 231, .7)",
                            "rgba(76, 185, 231, .6)",
                            "rgba(76, 185, 231, .5)",
                            "rgba(76, 185, 231, .4)",
                            "rgba(76, 185, 231, .3)"
                        ],
                        data: [data.countOfLandRequests,
                        data.countOfFarmlandRequests,
                        data.countOfCommercialUnitRequests,
                        data.countOfShopRequests,
                        data.countOfApartmentRequests]
                    }]
                },
                options: {
                    responsive: true
                }
            });

            var ctx9 = $("#doughnut-chart4").get(0).getContext("2d");
            var myChart9 = new Chart(ctx9, {
                type: "doughnut",
                data: {
                    labels: data.countOfRequestsPropertyStatuses.map(x => x.propertyStatus),
                    datasets: [{
                        backgroundColor: data.countOfRequestsPropertyStatuses.map(x => x.rgbColor),
                        data: data.countOfRequestsPropertyStatuses.map(x => x.count)
                    }]
                },
                options: {
                    responsive: true
                }
            });

            var ctx10 = $("#doughnut-chart5").get(0).getContext("2d");
            var myChart10 = new Chart(ctx10, {
                type: "doughnut",
                data: {
                    labels: ["Finalized", "Not finalized"],
                    datasets: [{
                        backgroundColor: [
                            "rgba(76, 185, 231, .7)",
                            "rgba(76, 185, 231, .5)"
                        ],
                        data: [data.countOfFinalizedListings.finalized, data.countOfFinalizedListings.notFinalized]
                    }]
                },
                options: {
                    responsive: true
                }
            });

            var ctx11 = $("#doughnut-chart6").get(0).getContext("2d");
            var myChart11 = new Chart(ctx11, {
                type: "doughnut",
                data: {
                    labels: ["Finalized", "Not finalized"],
                    datasets: [{
                        backgroundColor: [
                            "rgba(76, 185, 231, .7)",
                            "rgba(76, 185, 231, .5)"
                        ],
                        data: [data.countOfFinalizedRequests.finalized, data.countOfFinalizedRequests.notFinalized]
                    }]
                },
                options: {
                    responsive: true
                }
            });

        }
    });
}