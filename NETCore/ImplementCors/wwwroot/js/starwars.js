
$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    console.log(result)
    var text = "";
    $.each(result.results, (key, val) => {
        text += `
                <tr>
                    <td>${val.name}</td>
                    <td>${val.gender}</td>
                    <td>${val.height}</td>
                    <td>${val.mass}</td>
                    <td><a href="/LatihanCSS/Detail?id=${key+1}">Detail</a></td>
                </tr>`;

        $("#starwars_result").html(text)
    });
}).fail((err) => {
    console.log(err)
})