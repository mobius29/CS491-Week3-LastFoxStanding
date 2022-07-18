import {Unity, useUnityContext} from 'react-unity-webgl';
import './App.css';

function App() {
	const {unityProvider} = useUnityContext({
		loaderUrl: 'Build/build.loader.js',
		dataUrl: 'Build/build.data',
		frameworkUrl: 'Build/build.framework.js',
		codeUrl: 'Build/build.wasm',
	});

	return (
		<div className="App">
			<Unity className="unity" unityProvider={unityProvider} />
		</div>
	);
}

export default App;
