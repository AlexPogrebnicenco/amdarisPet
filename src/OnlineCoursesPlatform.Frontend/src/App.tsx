import { AuthProvider } from "./context/AuthContext";
import AxiosInterceptorWrapper from "./api/AxiosInterceptorWrapper";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import { CourseProvider } from "./context/CourseContext";

const App = () => {
  return (
    <AuthProvider>
      <CourseProvider>
        <AxiosInterceptorWrapper />
        <ToastContainer position="top-right" autoClose={3000} />
      </CourseProvider>
    </AuthProvider>
  );
};

export default App;
