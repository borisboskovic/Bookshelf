<template>
	<div class="loading-spinner__container">
		<div class="loading-spinner__spinner-element" id="spinner" />
		<div class="loading-spinner__caption">{{ caption }}</div>
	</div>
</template>

<script>
	import { onMounted } from "vue";
	import { Spinner } from "spin.js";
	import colors from "../../../assets/strings/colors";

	const opts = {
		lines: 8,
		length: 1,
		width: 40,
		radius: 40,
		scale: 1,
		corners: 1,
		speed: 1.2,
		rotate: 0,
		animation: "spinner-line-shrink",
		direction: 1,
		color: colors.accent,
		fadeColor: "transparent",
		top: "50%",
		left: "50%",
		shadow: "0 0 1px transparent",
		// zIndex: 2000000000,
		className: "loading-spinner__spinner-element",
		position: "relative",
	};

	const defaultCaption = "Loading...";

	export default {
		props: {
			text: String,
		},
		setup: (props) => {
			onMounted(() => {
				const target = document.getElementById("spinner");
				new Spinner(opts).spin(target);
			});

			const caption = props.text ? props.text : defaultCaption;

			return {
				caption,
			};
		},
	};
</script>

<style lang="scss">
	@import "./LoadingSpinner";
</style>
