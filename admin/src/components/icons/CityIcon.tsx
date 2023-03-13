import { IconButton, Tooltip } from '@chakra-ui/react';
import { FaCity } from "react-icons/fa";

interface IconProps {
  size?: string;
  fontSize?: string;
}

const CityIcon = ({size = "sm", fontSize = "18"}: IconProps) => {
  return (
    <Tooltip label="Cities">
      <IconButton
        variant="outline"
        size={size}
        fontSize={fontSize}
        colorScheme="blue"
        icon={<FaCity />}
        aria-label="Cities"
      />
    </Tooltip>
  );
}

export default CityIcon