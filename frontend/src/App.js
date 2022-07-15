import {useEffect, useState} from 'react';
import './App.css';
import IngamePage from './pages/IngamePage';
import {NodeGroup} from 'react-move';
import {easeExpInOut} from 'd3-ease';
import {interpolate, interpolateTransformSvg} from 'd3-interpolate';
import {scaleBand} from 'd3-scale';

const randomPlayers = () => {
	const ret = [];
	for (let i = 1; i <= 20; ++i) {
		const new_ret = ret.concat({
			id: i,
			color: '#' + (Math.random() * 0xffffff).toString(16),
			pos: {
				x: parseInt(Math.random() * 780) + 10,
				y: parseInt(Math.random() * 580) + 10,
			},
		});
	}
};

const App = () => {
	const view = [800, 600];
	const trbl = [10, 10, 10, 10];

	const dims = [view[0] - trbl[1] - trbl[3], view[1] - trbl[0] - trbl[2]];
	const [players, setPlayers] = useState(randomPlayers);

	const update = () => {
		setPlayers(randomPlayers);
	};

	/*
	useEffect(() => {
		const isKeyPressed = keyCode => {
			console.log(keyCode);
			setKeyPressed({
				[keyCode]: true,
				...keyPressed,
			});
		};

		const movePosition = key => {
			console.log(keyPressed);
			if (key === 'w') {
				setPos({x: pos.x - d, y: pos.y});
			}
			if (key === 'd') {
				setPos({x: pos.x, y: pos.y + d});
			}
		};

		const isKeyReleased = keyCode => {
			setKeyPressed({
				[keyCode]: false,
				...keyPressed,
			});
		};

		const onKeyDown = e => {
			if (keyPressed[87] === true && keyPressed[68] === true) {
				console.log('!23123');
				setPos({x: pos.x - d, y: pos.y + d});
				return;
			}

			if (e.key === 'w') {
				console.log('11111111111111111');
				movePosition(e.key);
				isKeyPressed(e.keyCode);

				console.log('222222222222222222');
			}
			if (e.key === 's') setPos({x: pos.x + d, y: pos.y});
			if (e.key === 'd') {
				movePosition(e.key);
				isKeyPressed(e.keyCode);
			}
		};

		const onKeyRelease = e => {
			console.log(e.key);
			isKeyReleased(e.keyCode);
		};

		document.addEventListener('keydown', onKeyDown);
		document.addEventListener('keyup', onKeyRelease);

		return () => {
			document.removeEventListener('keydown', onKeyDown);
			document.removeEventListener('keyup', onKeyRelease);
		};
	}, [players]);
	*/

	return (
		<div>
			<button onClick={update}>update</button>
		</div>
	);
};

export default App;
