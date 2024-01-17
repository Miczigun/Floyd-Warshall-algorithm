.data
S_MAX equ 0x3E80
n word ?
it word ?

.code
asmFloydWarshall proc
	mov n, dx
	mov it, bx

	xor     rax, rax
    jmp     outer_loop_end
outer_loop_start:
    ; Loop over j (inner loop)
    xor     rbx, rbx
    jmp     inner_loop_end

inner_loop_start:
    ; Compute indices
    mov     r10, rax               ; r10 = i
    imul    r10, r8                ; r10 = i * rAndC
    add     r10, r9                ; r10 = i * rAndC + iteration

    mov     r11, r9                ; r11 = iteration
    imul    r11, r8                ; r11 = iteration * rAndC
    add     r11, rbx               ; r11 = iteration * rAndC + j

    mov     r12, rax               ; r12 = i
    imul    r12, r8                ; r12 = i * rAndC
    add     r12, rbx               ; r12 = i * rAndC + j

    
    vmovdqu16  ymm0, [rdi + r10*2] ;Load resultGraph[i * rAndC + iteration]

    
    vmovdqu16  ymm1, [rdi + r11*2] ;Load resultGraph[iteration * rAndC + j]

    vmovdqu16  ymm2, [rdi + r12*2] ;Load resultGraph[i * rAndC + j]

    vpcmpeqw ymm3, ymm0, ymm0
    vpmovmskb r13d, ymm3

    test    r13d, r13d
    jz      inner_loop_increment

    ; Compute resultGraph[i * rAndC + iteration] + resultGraph[iteration * rAndC + j]
    vpaddd  ymm0, ymm0, ymm1

    ; Compare (resultGraph[i * rAndC + iteration] + resultGraph[iteration * rAndC + j]) < resultGraph[i * rAndC + j]
    vpcmpgtw ymm3, ymm2, ymm0
    vpmovmskb r13d, ymm3
    test    r13d, r13d
    jz      inner_loop_increment

    ; Update resultGraph[i * rAndC + j]
    vmovdqu16 [rdi + r12*2], ymm0

inner_loop_increment:
    inc     rbx
    cmp     rbx, r8
    jl      inner_loop_start

inner_loop_end:
    inc     rax
    cmp     rax, r8
    jl      outer_loop_start

outer_loop_end:
	ret

asmFloydWarshall endp
end