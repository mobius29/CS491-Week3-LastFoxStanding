import './Player.css';

const Player = ({top, left}) => {
	return (
		<div
			className="Player"
			style={{
				top: top,
				left: left,
			}}
		/>
	);
};

// background
export default Player;
