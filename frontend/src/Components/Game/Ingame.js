import React from 'react';
import './Ingame.css';
import Player from './Player';

const Ingame = ({pos}) => {
	return (
		<div className="Ingame">
			<Player top={pos.x} left={pos.y} />
		</div>
	);
};

export default Ingame;
