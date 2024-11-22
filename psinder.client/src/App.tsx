//import { useEffect, useState } from "react";
import Home from "./Components/Pages/Home";
import "./App.css";
import Navbar from "./Components/Nav/Navbar";
import Auth, { AuthProvider } from "./Helpers/Auth";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "./Components/Pages/Login";
import Register from "./Components/Pages/Register";

// interface Forecast {
//   date: string;
//   temperatureC: number;
//   temperatureF: number;
//   summary: string;
// }

// function App() {
//   const [forecasts, setForecasts] = useState<Forecast[]>();

//   useEffect(() => {
//     populateWeatherData();
//   }, []);

//   const contents =
//     forecasts === undefined ? (
//       <p>
//         <em>
//           Loading... Please refresh once the ASP.NET backend has started. See{" "}
//           <a href="https://aka.ms/jspsintegrationreact">
//             https://aka.ms/jspsintegrationreact
//           </a>{" "}
//           for more details.
//         </em>
//       </p>
//     ) : (
//       <table className="table table-striped" aria-labelledby="tableLabel">
//         <thead>
//           <tr>
//             <th>Date</th>
//             <th>Temp. (C)</th>
//             <th>Temp. (F)</th>
//             <th>Summary</th>
//           </tr>
//         </thead>
//         <tbody>
//           {forecasts.map((forecast) => (
//             <tr key={forecast.date}>
//               <td>{forecast.date}</td>
//               <td>{forecast.temperatureC}</td>
//               <td>{forecast.temperatureF}</td>
//               <td>{forecast.summary}</td>
//             </tr>
//           ))}
//         </tbody>
//       </table>
//     );

//   return (
//     <div>
//       <Navbar></Navbar>
//       <h1 id="tableLabel">Weather forecast</h1>
//       <p>This component demonstrates fetching data from the server.</p>
//       {contents}
//     </div>
//   );

//   async function populateWeatherData() {
//     const response = await fetch("/api/Shelters/GetAllShelters", {
//       method: "GET",
//     });
//     if (response.ok) {
//       const data = await response.json();
//       console.log(data);
//       setForecasts(data);
//     }
//   }
// }

const App = () => {
  return (
    <AuthProvider>
      <Router>
        <Navbar />
        <Routes>
          <Route element={<Auth />}>
            <Route path="/" element={<Home />} />
          </Route>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
