import { AuthProvider } from './context/AuthContext';
import AxiosInterceptorWrapper from './api/AxiosInterceptorWrapper';
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";

const App = () => {
  return (
    <AuthProvider>
      <AxiosInterceptorWrapper />
      <ToastContainer position="top-right" autoClose={3000} />
    </AuthProvider>
  );
};

export default App;
