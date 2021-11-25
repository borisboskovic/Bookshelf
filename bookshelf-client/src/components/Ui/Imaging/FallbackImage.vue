<template>
	<img
		:src="avatar"
		ref="imageRef"
		:alt="title"
		class="image-element"
		:style="{ objectFit: actualObjectFit }"
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
			objectFit: String,
		},
		setup: (props) => {
			const imageRef = ref();

			const loadingErrorHandler = () => {
				imageRef.value.src = props.defaultImage;
			};

			const actualObjectFit = computed(() => props.objectFit ?? "cover");

			const avatar = computed(() => props.source ?? props.defaultImage);

			return {
				imageRef,
				loadingErrorHandler,
				avatar,
				actualObjectFit,
			};
		},
	};
</script>

<style lang="scss" scoped>
	.image-element {
		width: 100%;
		height: 100%;
	}
</style>
