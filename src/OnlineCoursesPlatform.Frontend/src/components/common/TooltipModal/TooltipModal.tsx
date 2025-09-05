import { Tooltip, type TooltipProps } from "@mui/material";
import type { ReactNode } from "react";

interface TooltipModalProps {
  title: string;
  children: ReactNode;
  placement?: TooltipProps["placement"];
}

const TooltipModal: React.FC<TooltipModalProps> = ({
  title,
  children,
  placement = "bottom",
}) => {
  return (
    <Tooltip
      title={title}
      placement={placement}
      slotProps={{
        popper: {
          modifiers: [
            {
              name: "zIndex",
              enabled: true,
              phase: "beforeWrite",
              fn: ({ state }) => {
                state.styles.popper.zIndex = "2000";
              },
            },
          ],
        },
      }}
    >
      <div>{children}</div>
    </Tooltip>
  );
};

export default TooltipModal;
