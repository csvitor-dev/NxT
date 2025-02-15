<p>
    <img src="./assets/NxT.png" alt="NxT official brand" width="60" />
</p>

# _NxT_: Open Sales Manager

> Maintained by **NextTrend**

> [!IMPORTANT]\
> Just to clarify, **both the '_NxT_' project and the '_NextTrend_' "company" are fictitious!** The fiction is intended
> to provide a context for software development in line with what the team is learning in a college course:
> _Projeto Detalhado de Software (PDS)_ Detailed Software Design.
>
> Despite this, I put a lot of effort into making the fictional very tangible.

> [!NOTE]\
> The old project, called '**SalesWebMVC**' has been completely revamped, you can find the old `README.md` in
> [`docs/older/README.md`](./docs/older/README.md).

## How to run?

> As a first requirement to run the application, **you need to have [`docker`](https://docs.docker.com/get-started/) installed on your machine**.

First, make sure the **_Docker Compose_** services are running, using:

```bash
docker compose ps
```

If not:

```bash
# daemon mode (in backgroud)
docker compose up -d
```

Then, just run the **_ASP.NET MVC_** application!

```bash
# assuming it is at the root of the project, use the `--project` flag
dotnet run --project ./src/NxT.Mvc/ -lp https
```

> [!WARNING]\
> When you run the application as above, it will use the **_MySQL_** database provider by default.
> The application provides two other providers: **_PostgreSQL_** and **_SQL Server_**.
>
> To run any other provider, simply provide a new argument via the **.NET CLI**:
> ```bash
> # to run PostgreSQL
> dotnet run --project ./src/NxT.Mvc/ -lp https psql
>
> # or, to run SQL Server
> dotnet run --project ./src/NxT.Mvc/ -lp https mssql
> ```
>
> Any argument supplied that is not a valid provider `["mysql", "psql", "mssql"]` will default to **_MySQL_**.

---

<br />
<p align="center">
    <img src="./assets/NextTrend.png" alt="NxT official brand" width="45" />
</p>
