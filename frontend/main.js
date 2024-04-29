document.addEventListener("DOMContentLoaded", init);
const URL_API = "http://localhost:5084/api/";
var customers = [];
function init() {
  search();
}

function Add() {
  clean();
  openForm();
}
function openForm() {
  htmlModal = document.getElementById("modal");
  htmlModal.setAttribute("class", "modale opened");
}

function closeModal() {
  htmlModal = document.getElementById("modal");
  htmlModal.setAttribute("class", "modale");
}

async function search() {
  try {
    var url = URL_API + "customers";
    var response = await fetch(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    customers = await response.json();

    var html = "";
    for (customer of customers) {
      var row = `<tr>
      <td>${customer.firstName}</td>
      <td>${customer.lastName}</td>
      <td>${customer.email}</td>
      <td>${customer.phone}</td>
      <td>
        <a href="#" onclick="edit(${customer.id})" class="myButton">Editar</a>
        <a href="#" onclick="remove(${customer.id})" class="btnDelete">Eliminar</a>
      </td>
    </tr>`;
      html = html + row;
    }
    document.querySelector("#customers > tbody").outerHTML = html;
  } catch (error) {
    console.error("Error al buscar clientes:", error);
  }
}

async function edit(id) {
  openForm();
  var customer = customers.find((x) => x.id == id);
  console.log(customer);
  document.getElementById("txtId").value = customer.id;
  document.getElementById("txtFirstname").value = customer.firstName;
  document.getElementById("txtLastname").value = customer.lastName;
  document.getElementById("txtPhone").value = customer.phone;
  document.getElementById("txtAddress").value = customer.address;
  document.getElementById("txtEmail").value = customer.email;
}

async function remove(id) {
  try {
    respuesta = confirm("¿Está seguro de eliminarlo?");
    if (respuesta) {
      var url = URL_API + "customers/" + id;
      await fetch(url, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });
      window.location.reload();
    }
  } catch (error) {
    console.error("Error al eliminar cliente:", error);
  }
}

function clean() {
  document.getElementById("txtId").value = "";
  document.getElementById("txtFirstname").value = "";
  document.getElementById("txtLastname").value = "";
  document.getElementById("txtPhone").value = "";
  document.getElementById("txtAddress").value = "";
  document.getElementById("txtEmail").value = "";
}

async function save() {
  try {
    var data = {
      address: document.getElementById("txtAddress").value,
      email: document.getElementById("txtEmail").value,
      firstname: document.getElementById("txtFirstname").value,
      lastname: document.getElementById("txtLastname").value,
      phone: document.getElementById("txtPhone").value,
    };

    var id = document.getElementById("txtId").value;
    if (id != "") {
      data.id = id;
    }

    var url = URL_API + "customers";
    await fetch(url, {
      method: id != "" ? "PUT" : "POST",
      body: JSON.stringify(data),
      headers: {
        "Content-Type": "application/json",
      },
    });
    window.location.reload();
  } catch (error) {
    console.error("Error al guardar los datos:", error);
  }
}
