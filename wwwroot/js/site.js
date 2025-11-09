@section Scripts {
    <script>
        document.getElementById("empleadoSelect").addEventListener("change", function () {
            var id = this.value;
        if (id) {
            fetch(`/TuControlador/ObtenerNombreEmpleado?id=${id}`)
                .then(response => response.json())
                .then(data => {
                    document.getElementById("nombreEmpleado").value = data.nombre;
                });
            } else {
            document.getElementById("nombreEmpleado").value = "";
            }
        });
    </script>

    @{ await Html.RenderPartialAsync("_ValidationScriptsPartial"); }
}
