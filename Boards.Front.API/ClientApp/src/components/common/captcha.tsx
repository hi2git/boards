import React from "react";
import ReCAPTCHA from "react-google-recaptcha";

interface IProps {
	onChange: (token: string | null) => void;
}

const Captcha: React.FC<IProps> = ({ onChange }) => {
	return <ReCAPTCHA sitekey="6LdYXh8hAAAAAKcqNg7jlcm4qHa0b0Y1TyP5w9FB" onChange={onChange} />;
};

export default Captcha;
