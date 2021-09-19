


// Получение данных из файла
let param = "employees";
async function GetData() {
    let data;
    
    
    // отправляет запрос и получаем ответ
    const response = await fetch("/api/values/"+param,
        {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

    
    if (response.ok === true) {
        // получаем данные
         data = await response.json();
        console.log(data);

    }

    if (param == "department") {

        getTableDepartment();
    }
    function getTableDepartment() {

        const tbody = document.querySelector('#body_table');
        let newElement = ' ';
        for (const department of data) {
            newElement += `<tr> <td>  ${department.id}  </td>  <td>  ${department.name}  </td>   </tr>`;

        }
        tbody.innerHTML = newElement;
    }

    if (param == "employees") {

        getTableEmployees();
    }

    function getTableEmployees() {

        const tbody = document.querySelector('#body_table_emploes');
        let newElement = ' ';
        for (const employees of data) {
            newElement += `<tr> <td>  ${employees.fio}  </td>  <td>  ${employees.dp[0].name}  </td>   </tr>`;

        }
        tbody.innerHTML = newElement;
    }


}


GetData();