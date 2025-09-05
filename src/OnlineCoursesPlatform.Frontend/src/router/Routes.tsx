import { useRoutes, Navigate } from "react-router-dom";
import PrivateRoute from "./PrivateRoute";
import RoleRoute from "./RoleRoute";
import WelcomePage from "../pages/WelcomePage/WelcomePage";
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
import Admin from "../pages/Admin/Admin";
import RequestNewLinkPage from "../pages/Auth/RequestNewLinkPage/RequestNewLinkPage";
import ForgotPasswordPage from "../pages/Auth/ForgotPasswordPage/ForgotPasswordPage";
import MyCoursesTab from "../pages/Teacher/MyCourses/MyCoursesTab";
import AddCourseTab from "../pages/Teacher/AddCourse/AddCourseTab";
import CourseDetailsPage from "../pages/CourseDetails/CourseDetailsPage";
import LessonDetailsPage from "../pages/LessonDetails/LessonDetailsPage";
import GoogleCallbackPage from "../pages/Auth/Google/GoogleCallbackPage";
import BrowseCoursesTab from "../pages/Courses/BrowseCoursesTab";
import StartedCoursesTab from "../pages/Courses/Started/StartedCoursesTab";
import CompletedCoursesTab from "../pages/Courses/CompletedCoursesTab";
import EditCoursePage from "../pages/Teacher/EditCourse/EditCoursePage";

export const AppRoutes = () => {
  const routes = useRoutes([
    // Открытые маршруты
    { path: "/", element: <WelcomePage /> }, // Публичная welcome страница
    { path: "/login", element: <Login /> },
    { path: "/register", element: <Register /> },
    { path: "/set-password", element: <SetPasswordForm /> },
    { path: "/auth/request-new-link", element: <RequestNewLinkPage /> },
    { path: "/auth/forgot-password", element: <ForgotPasswordPage /> },
    { path: "/google-callback", element: <GoogleCallbackPage /> },

    // Защищённые маршруты
    {
      element: <PrivateRoute />,
      children: [
        {
          path: "/app", //  Все защищённые маршруты начинаются с /app
          element: <MainLayout />,
          children: [
            { index: true, element: <Navigate to="/app/home" /> }, //  Редирект на /app/home
            { path: "home", element: <Home /> },
            {
              path: "courses",
              element: <Courses />,
              children: [
                { index: true, element: <Navigate to="browse" /> },
                { path: "browse", element: <BrowseCoursesTab /> },
                { path: "started", element: <StartedCoursesTab /> },
                { path: "completed", element: <CompletedCoursesTab /> },
              ],
            },
            { path: "topics", element: <Topics /> },
            { path: "projects", element: <Projects /> },
            { path: "statistics", element: <Statistics /> },
            { path: "subscription", element: <Subscription /> },
            { path: "settings-page", element: <SettingsPage /> },
            { path: "about-us", element: <AboutUs /> },
            { path: "profile", element: <Profile /> },
            { path: "profile:certificates", element: <Profile /> },
            { path: "profile:settings", element: <Profile /> },
            {
              path: "course/:courseId",
              element: <CourseDetailsPage />,
              children: [
                { path: "lesson/:orderNumber", element: <LessonDetailsPage /> },
                { path: "lesson/:orderNumber/edit", element: null },
              ],
            },

            // Только для Teacher
            {
              element: <RoleRoute allowedRoles={["Teacher"]} />,
              children: [
                {
                  path: "teacher",
                  element: <Teacher />, // это твой tabs layout
                  children: [
                    { index: true, element: <Navigate to="my-courses" /> },
                    { path: "my-courses", element: <MyCoursesTab /> },
                    { path: "add-course", element: <AddCourseTab /> },
                  ],
                },
              ],
            },

            // Только для Admin
            {
              element: <RoleRoute allowedRoles={["Admin"]} />,
              children: [{ path: "admin", element: <Admin /> }],
            },
          ],
        },
      ],
    },
  ]);

  return routes;
};
