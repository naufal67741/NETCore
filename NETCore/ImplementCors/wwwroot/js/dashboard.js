$(document).ready(function () {
    $.ajax({
        "url": "https://localhost:44300/API/Persons/GetGender",
    }).done(res => {
        console.log(res.result);

        //set value
        $('#countGender').html(`${res.result.genderCount}`);
        // Pie Chart Gender
        var ctx = document.getElementById("myPieChart");
        var myPieChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ["Female", "Male"],
                datasets: [{
                    data: [res.result.femaleCounter, res.result.maleCounter],
                    backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
                    hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
                    hoverBorderColor: "rgba(234, 236, 244, 1)",
                }],
            },
            options: {
                maintainAspectRatio: false,
                tooltips: {
                    backgroundColor: "rgb(255,255,255)",
                    bodyFontColor: "#858796",
                    borderColor: '#dddfeb',
                    borderWidth: 1,
                    xPadding: 15,
                    yPadding: 15,
                    displayColors: false,
                    caretPadding: 10,
                },
                legend: {
                    display: false
                },
                cutoutPercentage: 80,
            },
        });
        
        
    });
    $.ajax({
        "url": "https://localhost:44300/API/Persons/GetPerson",
    }).done(res => {
        /*console.log(res.result[0].salary);*/
        var sal = []
        var fName = []
        for (r of res.result) {
            /*console.log(r.salary)*/
            sal.push(r.salary)
            fName.push(r.firstName)
        }
       
        var salaries = {
            series: [{
                name: 'Salary',
                data: sal
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    columnWidth: '50%',
                }
            }, dataLabels: {
                enabled: false
            },
            stroke: {
                width: 2
            },

            grid: {
                row: {
                    colors: ['#fff', '#f2f2f2']
                }
            }, xaxis: {
                labels: {
                    rotate: -45
                },
                categories: fName,
                tickPlacement: 'on'
            },
            yaxis: {
                title: {
                    text: 'Salary (Rp)',
                },
            },
            fill: {
                type: 'gradient', gradient: {
                    shade: 'light',
                    type: "horizontal",
                    shadeIntensity: 0.25,
                    gradientToColors: undefined,
                    inverseColors: true,
                    opacityFrom: 0.85,
                    opacityTo: 0.85,
                    stops: [50, 0, 100]
                },
            }
        };

        var chartsa = new ApexCharts(document.querySelector("#chartsalary"), salaries);
        chartsa.render();

    });

    $.ajax({
        "url": "https://localhost:44300/API/Persons/GetPerson",
    }).done(res => {
        /*console.log(res.result[0].salary);*/
        var gpa = []
        var fName = []
        for (r of res.result) {
            /*console.log(r.salary)*/
            console.log(r)
            gpa.push(r.gpa)
            fName.push(r.firstName)
        }

        var salaries = {
            series: [{
                name: 'GPA',
                data: gpa
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    columnWidth: '50%',
                }
            }, dataLabels: {
                enabled: false
            },
            stroke: {
                width: 2
            },

            grid: {
                row: {
                    colors: ['#fff', '#f2f2f2']
                }
            }, xaxis: {
                labels: {
                    rotate: -45
                },
                categories: fName,
                tickPlacement: 'on'
            },
            yaxis: {
                title: {
                    text: 'Univ',
                },
            },
            fill: {
                type: 'gradient', gradient: {
                    shade: 'light',
                    type: "horizontal",
                    shadeIntensity: 0.25,
                    gradientToColors: undefined,
                    inverseColors: true,
                    opacityFrom: 0.85,
                    opacityTo: 0.85,
                    stops: [50, 0, 100]
                },
            }
        };

        var chartsa = new ApexCharts(document.querySelector("#chartGPA"), salaries);
        chartsa.render();

    });
    
})