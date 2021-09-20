
const urlSearchParams = new URLSearchParams(window.location.search);
const params = Object.fromEntries(urlSearchParams.entries());
var url = "https://swapi.dev/api/people/" + params.id
console.log(url)
$.ajax({
    url: url
}).done((result) => {
    console.log(result)
    var text = "";
        text += `
                <tr>
                    <td>${result.name}</td>
                    <td>${result.gender}</td>
                    <td>${result.height}</td>
                    <td>${result.mass}</td>
                    <td>${result.birth_year}</td>
                </tr>
                <img src="${result}"/>
            `;

        $("#starwars_result").html(text)
}).fail((err) => {
    console.log(err)
})