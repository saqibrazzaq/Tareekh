import { IconButton, Tooltip } from '@chakra-ui/react';
import { FaMapMarkedAlt } from "react-icons/fa";

interface IconProps {
  size?: string;
  fontSize?: string;
}

const StateIcon = ({size = "sm", fontSize = "18"}: IconProps) => {
  return (
    <Tooltip label="States">
      <IconButton
        variant="outline"
        size={size}
        fontSize={fontSize}
        colorScheme="blue"
        icon={<FaMapMarkedAlt />}
        aria-label="States"
      />
    </Tooltip>
  );
}

export default StateIcon