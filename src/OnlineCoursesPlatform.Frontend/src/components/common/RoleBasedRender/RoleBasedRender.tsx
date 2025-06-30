import type { ReactNode } from 'react';
import { useAuth } from '../../../context/AuthContext';

interface RoleBasedRenderProps {
  allowedRoles: string[];
  children: ReactNode;
}

const RoleBasedRender = ({ allowedRoles, children }: RoleBasedRenderProps) => {
  const { role } = useAuth();

  if (!role || !allowedRoles.includes(role)) {
    return null;
  }

  return <>{children}</>;
};

export default RoleBasedRender;
