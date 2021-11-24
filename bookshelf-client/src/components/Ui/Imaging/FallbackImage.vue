<template>
	<img
		:src="avatar"
		ref="imageRef"
		:alt="title"
		class="image-element"
		@error="loadingErrorHandler"
	/>
</template>

<script>
	import { ref, computed } from "vue";

	export default {
		props: {
			defaultImage: String,
			source: String,
			className: String,
			title: String,
		},
		setup: (props) => {
			const imageRef = ref();

			const loadingErrorHandler = () => {
				imageRef.value.src = props.defaultImage;
			};

			const avatar = computed(() => props.source ?? props.defaultImage);

			return {
				imageRef,
				loadingErrorHandler,
				avatar,
			};
		},
	};
</script>

<style lang="scss" scoped>
	.image-element {
		background-size: cover;
		width: 100%;
		height: 100%;
		object-fit: cover;
	}
</style>
