import { IconButton, Tooltip } from '@chakra-ui/react';
import { HiOutlineTranslate } from "react-icons/hi";

interface IconProps {
  size?: string;
  fontSize?: string;
}

const TranslationIcon = ({size = "sm", fontSize = "18"}: IconProps) => {
  return (
    <Tooltip label="Languages">
      <IconButton
        variant="outline"
        size={size}
        fontSize={fontSize}
        colorScheme="blue"
        icon={<HiOutlineTranslate />}
        aria-label="Languages"
      />
    </Tooltip>
  );
}

export default TranslationIcon