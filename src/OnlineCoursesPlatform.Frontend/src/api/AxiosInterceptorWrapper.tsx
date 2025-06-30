import { useAxiosInterceptor } from './axiosInterceptor';
import { AppRoutes } from '../router/Routes';

const AxiosInterceptorWrapper = () => {
  useAxiosInterceptor(); //  useAuth внутри AuthProvider
  return <AppRoutes />;
};

export default AxiosInterceptorWrapper;
