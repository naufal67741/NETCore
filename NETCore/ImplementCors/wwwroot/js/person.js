$(document).ready(function () {
    var table = $('#table_id').DataTable({
        "dom": 'Bfrtip',
        "buttons": [
            /*'copy', 'csv', 'excel', 'pdf', 'print'*/
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0,1,2,3,4]
                },
                className: 'btn btn-sm btn-outline-secondary',
                bom: true
            }
        ],
        "filter": true,        
        "ajax": {
            "url": "https://localhost:44300/API/Persons/GetPerson/",
            "datatype": "json",
            "dataSrc": "result"
        },
        "columns": [
            {
                "data": "nik",
                "width":"10%"
            },
            {
                "data": null,
                "width": "100%",
                "render": function (data, type, row) {
                    
                    return data["fullName"]
                },
                "autoWidth": true
            },
            {
                "data": null,
                "width": "1%",
                /*"orderable": false,*/
                "render": function (data, type, row) {
                    return data["gender"]
                },
                "autoWidth": true
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (data["phone"][0] == 0)
                        return "+62"+ data["phone"].substring(1)
                    return data["phone"]
                },
                "autoWidth": true
            },
            {
                "data": "email"
            },
            {
                "targets": -1,
                "data": null,
                /*"defaultContent": "<button>Click!</button>"*/
                "render": function (data, type, row) {
                    return `<button
                            type="button"
                            class="item-detail btn btn-primary"
                            data-toggle="modal"
                            data-target="#personDetailModal"
                            onClick="personDetail('${data["nik"]}')">
                            DETAIL</div>`
                },
            },
            {
                "targets": -1,
                "data": null,
                /*"defaultContent": "<button>Click!</button>"*/
                "render": function (data, type, row) {
                    return `<button
                            type="button"
                            class="item-detail btn btn-danger"
                            onClick="personDelete('${data["nik"]}')">
                            DELETE</div>`
                },
            }

        ]
    });
    $("#formRegister").submit(function (e) {
        e.preventDefault()
        var obj_register = {
            "Email": $("#inputEmail").val(),
            "NIK": $("#inputNIK").val(),
            "FirstName": $("#inputFName").val(),
            "LastName": $("#inputLName").val(),
            "Phone": $("#inputPhone").val(),
            "BirthDate": $("#inputDOB").val(),
            "Salary": parseInt($("#inputSalary").val()),
            "Password": $("#inputPassword").val(),
            "Degree": $("#inputDegree").val(),
            "GPA": $("#inputGPA").val(),
            "UniversityId": parseInt($("#inputUniversity").val()),
        }
        console.log(JSON.stringify(obj_register))

        $.ajax({
            url: "https://localhost:44300/API/Persons/Register",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json',
            crossDomain: true,
            data: JSON.stringify(obj_register),
            success: function (data) {
                $('#registerModal').modal('hide')
                Swal.fire({
                    title: 'Success Inserting Data!',
                    text: 'Press Any Button to Continue',
                    icon: 'success',
                    confirmButtonText: 'Okay'
                })
                setInterval(function () {
                    table.ajax.reload(null, false); // user paging is not reset on reload
                }, 1000);

                /*location.reload();*/
            },
            error: /*function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            if (err.errors.FirstName != undefined) {
                document.getElementById("inputFirstName").className = "form-control is-invalid";
                document.getElementById("inputFirstName").style("border: 10px solid red")
            }
        }*/
                console.log("error")

        })
    })
});



$.ajax({
    url: "https://localhost:44300/API/Universities/"
}).done(result => {
    text = ''
    $.each(result.data, function (key, val) {
        console.log(val.name)
        text += `<option value= "${val.universityId}">${val.name}</option>`
    })
    $('#inputUniversity').html(text)
}).fail(result => {
    console.log(result)
})

const personDetail = (nik) => {
    $.ajax({
        url: "https://localhost:44300/API/Persons/GetPerson/"+nik
    }).done(res => {
        data = res.result
        let pokeDetailContent = `
                                    ${data.fullName}<br>
                                    ${data.email}<br>
                                    ${data.phone}<br>
                                    ${data.gender}<br>
                                    ${data.salary}<br>
                                `;

        $('#personDetailModal .modal-body').html(pokeDetailContent);
        $('h5.modal-title').html(`Detail Profile of ${data.fullName}`);
    });
}

/*const personDelete = (nik) => {
    Swal.fire({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes, I am sure!',
        cancelButtonText: "No, cancel it!",
        closeOnConfirm: false,
        closeOnCancel: false
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:44300/API/Persons/" + nik,
                type: 'DELETE',
            }).done(res => {
                alert("asd")
            }).fail(err => {
                alert("fail")
            });
        }
    })
    
}*/

const personDelete = (nik) => {
    Swal.fire({
        title: 'Yakin, ingin menghapus data ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:44300/API/Persons/" + nik,
                type: "DELETE",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            }).done((result) => {
                Swal.fire({
                    title: 'Success!',
                    text: 'Data Has Been Deleted',
                    icon: 'success',
                    confirmButtonText: 'Next'
                })
                $('#table_id').DataTable().ajax.reload();
            }).fail((error) => {
                Swal.fire({
                    title: 'Error!',
                    text: 'Data Cannot Deleted',
                    icon: 'Error',
                    confirmButtonText: 'Next'
                })
            });
        }
    })

}

//manggil form

/*$.ajax({
    url: 'https://localhost:44300/API/Persons/GetPerson/'
}).done(res => {
    let htmlContent = "";
    console.log(res.results)

    $.each(res.result, (key, value) => {
        htmlContent += `<tr>
                            <td>${key + 1}</td>
                            <td>${value.fullName}</td>
                            <td>${value.gender}</td>
                            <td>${value.phone}</td>
                            <td>
                                <button
                                    type="button"
                                    class="item-detail btn btn-primary"
                                    data-toggle="modal"
                                    data-target="#personDetailModal"
                                    onClick="personDetail('${value}')">Detail</button>
                            </td>
                        </tr>`
    });

    $('.data-person').html(htmlContent)
})


const personDetail = (value) => {
    let pokeDetailContent = `
                                <img
                                    src="${res.sprites.other.dream_world.front_default}"
                                    class="mx-auto d-block mb-3"
                                    alt="pokeimg"
                                    width="350px"
                                    height="350px" />
                                    <div class="text-center">${types}</div>
                                <ul>
                                    <li>Name: ${value.fullName} </li>
                                    <li>Weight: ${res.height} </li>
                                    <li>Height: ${res.weight}</li>
                                    <li>Abilities:
                                        <ul>
                                            ${abilities}
                                        </ul>
                                    </li>
                                </ul>`;

    $('#personDetailModal .modal-body').html(pokeDetailContent);
    $('h5.modal-title').html(`${res.name}`);
}

$.ajax({
    url: 'https://localhost:44300/API/Persons/GetPerson/'
}).done(res => {
    for (let r of res.result) {
        console.log(r)
    }
    console.log(res.result)
})*/