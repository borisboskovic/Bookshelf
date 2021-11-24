<template>
	<div class="ratings-container">
		<SimpleSpinner v-if="isSubmitting" :scale="3" />
		<div>
			Your rating: <strong>{{ yourRating ?? "Not rated" }}</strong>
		</div>
		<div>
			Average rating: <strong>{{ average?.toFixed(1) ?? "Not rated" }}</strong>
		</div>
		<div class="stars-container">
			<StarRating
				:key="starKey"
				:rating="visibleRating"
				:round-start-rating="false"
				:show-rating="false"
				:star-size="40"
				:active-color="starColor"
				@update:rating="ratingChangeHandler"
			/>
			<span v-if="visibleRating > 0">{{ visibleRating?.toFixed(1) }}</span>
		</div>
	</div>
</template>

<script>
	import { ref, computed } from "vue";
	import colors from "@/assets/strings/colors";
	import StarRating from "vue-star-rating";
	import SimpleSpinner from "@/components/Ui/Spinners/SimpleSpinner.vue";

	export default {
		props: {
			average: Number,
			yourRating: Number,
			isSubmitting: Boolean,
		},
		components: {
			StarRating,
			SimpleSpinner,
		},
		emits: ["rating"],
		setup: (props, context) => {
			const starKey = ref(1);
			const visibleRating = computed(() => props.yourRating ?? props.average ?? 0);

			const starColor = computed(() =>
				props.yourRating ? colors.accent : colors.defaultStar
			);

			const ratingChangeHandler = (value) => {
				starKey.value += 1;
				context.emit("rating", value);
			};

			return {
				visibleRating,
				starColor,
				starKey,
				ratingChangeHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./StarRatingComponent";
</style>
