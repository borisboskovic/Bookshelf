<template>
	<div class="autor-info-container">
		<div class="lifespan-container">
			<div class="date-of-birth-container">
				<div class="label">Born</div>
				<div class="date-container">{{ formatedDOB }}</div>
			</div>
			<div class="separator">-</div>
			<div class="date-of-death-container">
				<div class="label">Died</div>
				<div class="date-container">{{ formatedDOD }}</div>
			</div>
		</div>
		<div class="place-of-birth">{{ placeOfBirth }}</div>
	</div>
</template>

<script>
	import { computed } from "vue";

	const dateFormatOptions = {
		day: "numeric",
		month: "short",
		year: "numeric",
	};

	export default {
		props: {
			dateOfBirth: String,
			dateOfDeath: String,
			placeOfBirth: String,
		},
		setup: (props) => {
			const formatedDOB = computed(() =>
				props.dateOfBirth
					? new Date(props.dateOfBirth).toLocaleDateString("sr-latn", dateFormatOptions)
					: ""
			);
			const formatedDOD = computed(() =>
				props.dateOfDeath
					? new Date(props.dateOfDeath).toLocaleDateString("sr-latn", dateFormatOptions)
					: ""
			);

			return {
				formatedDOB,
				formatedDOD,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BasicAuthorInfo";
</style>
