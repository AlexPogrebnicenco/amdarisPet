import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

interface Props {
  allowedRoles: string[];
}

const RoleRoute = ({ allowedRoles }: Props) => {
  const { role } = useAuth(); // Читаем из контекста

  if (!role || !allowedRoles.includes(role)) {
    return <Navigate to="/" />;
  }

  return <Outlet />;
};

export default RoleRoute;
