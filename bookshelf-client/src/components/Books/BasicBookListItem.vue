<template>
	<div class="item-container">
		<router-link :to="bookLink">
			<div class="book-cover-container">
				<FallbackImage :defaultImage="defaultImage" :source="image" :title="title" />
			</div>
		</router-link>
		<div class="book-title">
			<router-link :to="bookLink">
				{{ title }}
			</router-link>
		</div>
		<div class="details-container">
			<div class="rating-container">
				<i class="fa fa-star"></i>
				{{ visibleRating ?? "Not rated" }}
			</div>
			<template v-if="visibleRating">
				<span> &middot; </span>
				<span class="reviews-container"
					>{{ `${ratingsCount} rating${ratingsCount > 1 ? "s" : ""}` }}
				</span>
			</template>
		</div>
	</div>
</template>

<script>
	import { computed } from "vue";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import defaultImage from "@/assets/images/rasters/book-placeholder.jpg";

	export default {
		props: {
			id: Number,
			title: String,
			image: String,
			rating: Number,
			ratingsCount: Number,
		},
		components: {
			FallbackImage,
		},
		setup: (props) => {
			const visibleRating = props.rating ? props.rating.toFixed(1) : null;
			const bookLink = computed(() => `/book-details/${props.id}`);

			return {
				defaultImage,
				visibleRating,
				bookLink,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BasicBookListItem.scss";
</style>
