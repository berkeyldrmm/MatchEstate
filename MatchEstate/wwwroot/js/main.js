(function ($) {
    "use strict";
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    
    
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    $('.sidebar-toggler').click(function () {
        $('.sidebar, .content').toggleClass("open");
        return false;
    });

    $('.pg-bar').waypoint(function () {
        $('.progress .progress-bar').each(function () {
            $(this).css("width", $(this).attr("aria-valuenow") + '%');
        });
    }, {offset: '80%'});


    $('#calender').datetimepicker({
        inline: true,
        format: 'L'
    });


    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        items: 1,
        dots: true,
        loop: true,
        nav : false
    });


    Chart.defaults.color = "#6C7293";
    Chart.defaults.borderColor = "#000000";


    $(".canvas").each((index, value) => {
        value.innerHTML = '<div class="show d-flex align-items-center justify-content-center w-100" > <div class="spinner-border text-primary text-center" style = "width: 3rem; height: 3rem;" role = "status" > <span class="sr-only" > Loading...</span></div > </div>';

    });

    if (window.location.pathname == "/") {
        $.get("/getStatistics", (data, status) => {
            console.log(data);
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

            }
        });
    }
    
})(jQuery);