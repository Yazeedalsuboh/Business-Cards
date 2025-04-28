# Business Cards

## Setup Instructions

### Pre-requisites

Before setting up the project, ensure that you have the following installed on your system:

-   **Node.js** and **npm** (for the frontend)
-   **Angular CLI** (for the frontend)
-   **.NET 9 SDK** and **Visual Studio** (for the backend)

### Process

#### Running the Frontend

1. Clone the repository:

    ```
    git clone <repository_url>
    cd business-cards
    ```

2. Navigate to the frontend directory:

    ```
    cd frontend
    ```

3. Install the required npm packages:

    ```
    npm install
    ```

4. Open the project in your code editor (e.g., Visual Studio Code):

    ```
    code .
    ```

5. Run the Angular frontend:

    ```
    ng serve
    ```

    This will start the frontend application at `http://localhost:4200`.

#### Running the Backend

1. Navigate to the backend directory:

    ```
    cd backend
    ```

2. Open the backend project in Visual Studio by running the solution file:

    ```
    <your_solution_file.sln>
    ```

3. Open the **Package Manager Console** in Visual Studio and run the following commands to set up the database:

    ```
    Add-Migration
    Update-Database
    ```

4. Start debugging by pressing **F5**. This will run the backend on a local port (e.g., `7175`).

5. After the backend is running, find the port number in the command line or output window.

6. Update the API URL in the frontend environment file:

    - Navigate to the frontend code and find the `environment.ts` file.
    - Change the `apiUrl` to match the port of your local backend. For example:

    ```typescript
    apiUrl: "https://localhost:7175";
    ```

## Implementation

### Frontend (Angular 19)

The frontend of the application is built using Angular 19. It handles the user interface and communicates with the backend API to manage business card data. The frontend provides the following features:

-   The application consists of 3 pages that can be accessed via the menu:
    1. **List Page**: Lists all business cards and provides filtering options. Users can delete a business card or export it as XML or CSV.
    2. **Form Page**: Allows users to manually add a new business card.
    3. **Import Page**: Allows users to import business cards using XML or CSV files.
-   The API endpoints are called from the `ApiService` to handle business card data.

### Backend (.NET 9)

The backend is built using .NET 9 and serves as the API for the frontend. It handles the following:

-   The API contains 6 endpoints that allow for CRUD operations and file import/export.

#### API Endpoints

1. **Get All Business Cards**

    - **URL:** `GET /api/businesscards`
    - **Functionality:** Fetches all business cards stored in the database.

2. **Add a New Business Card**

    - **URL:** `POST /api/businesscards`
    - **Functionality:** Adds a new business card to the database by receiving the data from the request body (form data).

3. **Remove a Business Card**

    - **URL:** `DELETE /api/businesscards/{id}`
    - **Functionality:** Deletes a business card from the database using the provided `id`.

4. **Export Business Card to CSV**

    - **URL:** `GET /api/businesscards/export/csv/{id}`
    - **Functionality:** Exports a business card to a CSV file using the `id`. It returns the file for download.

5. **Export Business Card to XML**

    - **URL:** `GET /api/businesscards/export/xml/{id}`
    - **Functionality:** Exports a business card to an XML file using the `id`. It returns the file for download.

6. **Import Business Cards from CSV**

    - **URL:** `POST /api/businesscards/import/csv`
    - **Functionality:** Allows importing business cards from a CSV file. The file is uploaded as form data.

7. **Import Business Cards from XML**

    - **URL:** `POST /api/businesscards/import/xml`
    - **Functionality:** Allows importing business cards from an XML file. The file is uploaded as form data.

8. **Filter Business Cards**
    - **URL:** `GET /api/businesscards/filter`
    - **Functionality:** Filters business cards based on the provided `term` and `searchString`. Returns the matching business cards.
