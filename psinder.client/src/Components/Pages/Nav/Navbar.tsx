import { Button } from "../../ui/button";
import { useAuth } from "../../../Helpers/Contexts/AuthContext";
import { Switch } from "../../ui/switch";
import { Label } from "../../ui/label";
import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../../../App.css";
import logo from "../../../assets/logo.png";
import { shelterIdContext } from "../../../Helpers/Contexts/ShelterContext";

const Navbar = () => {
  const [darkMode, setDarkMode] = useState(true);
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();
  const { shelterId } = shelterIdContext();
  const handleLogout = async () => {
    await logout();
    navigate("/");
  };

  useEffect(() => {
    darkMode
      ? document.documentElement.classList.add("dark")
      : document.documentElement.classList.remove("dark");
  }, [darkMode]);

  const toggleDarkMode = () => setDarkMode(!darkMode);

  return (
    <div>
      <header>
        <div className="container">
          <nav
            className={`${
              darkMode
                ? "bg-gradient-to-b from-green-900 from-0% to-green-950 to-100%"
                : "bg-gradient-to-b from-green-500 from-0% to-green-700 to-100%"
            } max-h-20`}
          >
            <div className="flex gap-4">
              <Link to="/">
                <img
                  className="mt-3 ml-4"
                  src={logo}
                  width="48"
                  height="64"
                  alt="logo"
                ></img>
              </Link>
              <div className="grid grid-cols-[100px_1fr] flex">
                <h1 className="mt-0 translate-y-2">
                  <Link to="/">Psinder</Link>
                </h1>
                <ul className="max-h-20">
                  <li>
                    <Link to="/">Home</Link>
                  </li>

                  <li>
                    <Link to="/pets">All pets</Link>
                  </li>
                  <li>
                    <Link to="/about">About</Link>
                  </li>
                </ul>
              </div>
            </div>

            <div className="flex flex-box">
              <div className="mt-4 space-x-5">
                {isAuthenticated ? (
                  <>
                    {shelterId != null ? (
                      <>
                        <Link to="/addPet">
                          <Button
                            className={`${
                              darkMode ? "bg-green-600" : "bg-green-400"
                            } text-lg w-36 h-10 gap-2`}
                          >
                            Add new pet
                            <svg
                              xmlns="http://www.w3.org/2000/svg"
                              className="h-6 w-6"
                              fill="none"
                              viewBox="0 0 24 24"
                              stroke="currentColor"
                            >
                              <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                strokeWidth="2"
                                d="M12 4v16m8-8H4"
                              />
                            </svg>
                          </Button>
                        </Link>
                        <Link to={`/shelter/edit/${shelterId}`}>
                          <Button
                            className={`${
                              darkMode ? "bg-green-600" : "bg-green-400"
                            } text-lg w-36 h-10 `}
                          >
                            View your shelter
                          </Button>
                        </Link>
                      </>
                    ) : (
                      <Link to="/shelter/add">
                        <Button
                          className={`${
                            darkMode ? "bg-green-600" : "bg-green-400"
                          } text-lg w-36 h-10 `}
                        >
                          Register a shelter
                        </Button>
                      </Link>
                    )}
                    <Button
                      className={`${
                        darkMode ? "bg-green-600" : "bg-green-400"
                      } text-lg w-24 h-10 `}
                      onClick={handleLogout}
                    >
                      Log out
                    </Button>
                  </>
                ) : (
                  <>
                    <Link to="/login">
                      <Button
                        className={`${
                          darkMode ? "bg-green-600" : "bg-green-400"
                        } text-lg w-24 h-10 `}
                      >
                        Log In
                      </Button>
                    </Link>
                    <Link to="/register">
                      <Button
                        className={`${
                          darkMode ? "bg-green-600" : "bg-green-400"
                        } text-lg w-24 h-10 `}
                      >
                        Sign up
                      </Button>
                    </Link>
                  </>
                )}
              </div>
              <div className="w-[100px] flex flex-col items-center space-y-2 m-4 pt-1">
                <Switch
                  className="data-[state=unchecked]:bg-green-800 data-[state=checked]:bg-green-500"
                  onCheckedChange={toggleDarkMode}
                />
                <Label htmlFor="dark-mode">
                  {darkMode ? "Light mode" : "Dark mode"}
                </Label>
              </div>
            </div>
          </nav>
        </div>
      </header>
    </div>
  );
};

export default Navbar;
