import Home from "./Components/Pages/Nav/Home";
import "./App.css";
import Navbar from "./Components/Pages/Nav/Navbar";
import Auth, { AuthProvider } from "./Helpers/Contexts/AuthContext";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "./Components/Pages/Login";
import Register from "./Components/Pages/Register";
import AddEditShelter from "./Components/Pages/Shelter/AddEditShelter";
import Pets from "./Components/Pages/Pet/Pets";
import About from "./Components/Pages/Nav/About";
import { ShelterProvider } from "./Helpers/Contexts/ShelterContext";
import AddEditPet from "./Components/Pages/Pet/AddEditPet";
import PetDetails from "./Components/Pages/Pet/PetDetails";

const App = () => {
  return (
    <AuthProvider>
      <Router>
        <ShelterProvider>
          <Navbar />
        </ShelterProvider>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route element={<Auth />}>
            <Route path="/shelter/add" element={<AddEditShelter />} />
            <Route path="/shelter/edit/:id" element={<AddEditShelter />} />
            <Route path="/pet/add" element={<AddEditPet />} />
            <Route path="/pet/edit/:id" element={<AddEditPet />} />
          </Route>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />

          <Route path="/pets" element={<Pets />} />
          <Route path="/about" element={<About />} />
          <Route path="/pets/:id" element={<PetDetails />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
