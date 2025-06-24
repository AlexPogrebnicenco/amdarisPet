import { useRoutes } from "react-router-dom";
import Login from "../pages/Auth/Login/Login";
import Register from "../pages/Auth/Register/Register";
import MainLayout from "../layouts/MainLayout";
import Home from "../pages/Home/Home";
import Courses from "../pages/Courses/Courses";
import Topics from "../pages/Topics/Topics";
import Projects from "../pages/Projects/Projects";
import Statistics from "../pages/Statistics/Statistics";
import Subscription from "../pages/Subscription/Subscription";
import SettingsPage from "../pages/SettingsPage/SettingsPage";
import AboutUs from "../pages/AboutUs/AboutUs";
import Profile from "../pages/Profile/Profile";
import SetPasswordForm from "../pages/Auth/SetPassword/SetPasswordForm";
import Teacher from "../pages/Teacher/Teacher";
import Admin from "../pages/Admin";

export const AppRoutes = () => {
  const routes = useRoutes([
    {
      path: "/",
      element: <MainLayout />,
      children: [
        { path: "home", element: <Home /> },
        { path: "courses", element: <Courses /> },
        { path: "topics", element: <Topics /> },
        { path: "projects", element: <Projects /> },
        { path: "statistics", element: <Statistics /> },
        { path: "subscription", element: <Subscription /> },
        { path: "settings-page", element: <SettingsPage /> },
        { path: "about-us", element: <AboutUs /> },
        // Profile main and tab routes
        { path: "profile", element: <Profile /> },
        { path: "profile:certificates", element: <Profile /> },
        { path: "profile:settings", element: <Profile /> },

        //Teacher path
        { path: "teacher", element: <Teacher /> },

        //Admin path 
        { path: "admin", element: <Admin />}
      ],
    },
    { path: "/login", element: <Login /> },
    { path: "/register", element: <Register /> },
    { path: "/set-password", element: <SetPasswordForm /> }, // Удалить после тестов
  ]);

  return routes;
};
